/**
 * =============================================================================
 * CS2Fixes
 * Copyright (C) 2023 Source2ZE
 * =============================================================================
 *
 * This program is free software; you can redistribute it and/or modify it under
 * the terms of the GNU General Public License, version 3.0, as published by the
 * Free Software Foundation.
 *
 * This program is distributed in the hope that it will be useful, but WITHOUT
 * ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
 * FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more
 * details.
 *
 * You should have received a copy of the GNU General Public License along with
 * this program.  If not, see <http://www.gnu.org/licenses/>.
 */

#include "schema.h"

#include "interfaces/cs2_interfaces.h"
#include "core/globals.h"
#include "core/memory.h"
#include "core/log.h"

#include "entity2/entityidentity.h"
#include "entity2/entityinstance.h"
#include "tier1/utlmap.h"
#include <schemasystem.h>

// memdbgon must be the last include file in a .cpp file!!!
#include "tier0/memdbgon.h"

using SchemaKeyValueMap_t = CUtlMap<uint32_t, SchemaKey>;
using SchemaTableMap_t = CUtlMap<uint32_t, SchemaKeyValueMap_t*>;

namespace
{
static_assert(sizeof(CEntityIdentity) == 0x70, "CEntityIdentity layout changed");
static_assert(offsetof(CEntityIdentity, m_pInstance) == 0x0, "CEntityIdentity::m_pInstance offset changed");
static_assert(offsetof(CEntityIdentity, m_pClass) == 0x8, "CEntityIdentity::m_pClass offset changed");
static_assert(offsetof(CEntityIdentity, m_EHandle) == 0x10, "CEntityIdentity::m_EHandle offset changed");
static_assert(offsetof(CEntityIdentity, m_nameStringableIndex) == 0x14, "CEntityIdentity::m_nameStringableIndex offset changed");
static_assert(offsetof(CEntityIdentity, m_name) == 0x18, "CEntityIdentity::m_name offset changed");
static_assert(offsetof(CEntityIdentity, m_designerName) == 0x20, "CEntityIdentity::m_designerName offset changed");
static_assert(offsetof(CEntityIdentity, m_flags) == 0x30, "CEntityIdentity::m_flags offset changed");
static_assert(offsetof(CEntityIdentity, m_worldGroupId) == 0x38, "CEntityIdentity::m_worldGroupId offset changed");
static_assert(offsetof(CEntityIdentity, m_fDataObjectTypes) == 0x3C, "CEntityIdentity::m_fDataObjectTypes offset changed");
static_assert(offsetof(CEntityIdentity, m_PathIndex) == 0x40, "CEntityIdentity::m_PathIndex offset changed");
static_assert(offsetof(CEntityIdentity, m_pPrev) == 0x50, "CEntityIdentity::m_pPrev offset changed");
static_assert(offsetof(CEntityIdentity, m_pNext) == 0x58, "CEntityIdentity::m_pNext offset changed");
static_assert(offsetof(CEntityIdentity, m_pPrevByClass) == 0x60, "CEntityIdentity::m_pPrevByClass offset changed");
static_assert(offsetof(CEntityIdentity, m_pNextByClass) == 0x68, "CEntityIdentity::m_pNextByClass offset changed");
static_assert(offsetof(CEntityInstance, m_iszPrivateVScripts) == 0x8, "CEntityInstance::m_iszPrivateVScripts offset changed");
static_assert(offsetof(CEntityInstance, m_pEntity) == 0x10, "CEntityInstance::m_pEntity offset changed");
static_assert(offsetof(CEntityInstance, m_pKeyValues) == 0x20, "CEntityInstance::m_pKeyValues offset changed");
static_assert(offsetof(CEntityInstance, m_CScriptComponent) == 0x30, "CEntityInstance::m_CScriptComponent offset changed");

constexpr uint32_t kCEntityIdentity = hash_32_fnv1a_const("CEntityIdentity");
constexpr uint32_t kCEntityInstance = hash_32_fnv1a_const("CEntityInstance");

constexpr uint32_t kField_m_pInstance = hash_32_fnv1a_const("m_pInstance");
constexpr uint32_t kField_m_pClass = hash_32_fnv1a_const("m_pClass");
constexpr uint32_t kField_m_EHandle = hash_32_fnv1a_const("m_EHandle");
constexpr uint32_t kField_m_nameStringableIndex = hash_32_fnv1a_const("m_nameStringableIndex");
constexpr uint32_t kField_m_name = hash_32_fnv1a_const("m_name");
constexpr uint32_t kField_m_designerName = hash_32_fnv1a_const("m_designerName");
constexpr uint32_t kField_m_flags = hash_32_fnv1a_const("m_flags");
constexpr uint32_t kField_m_worldGroupId = hash_32_fnv1a_const("m_worldGroupId");
constexpr uint32_t kField_m_fDataObjectTypes = hash_32_fnv1a_const("m_fDataObjectTypes");
constexpr uint32_t kField_m_PathIndex = hash_32_fnv1a_const("m_PathIndex");
constexpr uint32_t kField_m_pAttributes = hash_32_fnv1a_const("m_pAttributes");
constexpr uint32_t kField_m_pPrev = hash_32_fnv1a_const("m_pPrev");
constexpr uint32_t kField_m_pNext = hash_32_fnv1a_const("m_pNext");
constexpr uint32_t kField_m_pPrevByClass = hash_32_fnv1a_const("m_pPrevByClass");
constexpr uint32_t kField_m_pNextByClass = hash_32_fnv1a_const("m_pNextByClass");

constexpr uint32_t kField_m_iszPrivateVScripts = hash_32_fnv1a_const("m_iszPrivateVScripts");
constexpr uint32_t kField_m_pEntity = hash_32_fnv1a_const("m_pEntity");
constexpr uint32_t kField_m_pKeyValues = hash_32_fnv1a_const("m_pKeyValues");
constexpr uint32_t kField_m_CScriptComponent = hash_32_fnv1a_const("m_CScriptComponent");

static bool TryGetNativeEntitySchemaOffset(uint32_t classKey, uint32_t memberKey, SchemaKey& key)
{
    // The April 2026 update changed runtime schema field records, but these
    // foundational entity layouts are already defined in hl2sdk and are more
    // stable to bind against directly than the live schema data.
    if (classKey == kCEntityIdentity)
    {
        switch (memberKey)
        {
            case kField_m_pInstance:
                key = { static_cast<int32_t>(offsetof(CEntityIdentity, m_pInstance)), false };
                return true;
            case kField_m_pClass:
                key = { static_cast<int32_t>(offsetof(CEntityIdentity, m_pClass)), false };
                return true;
            case kField_m_EHandle:
                key = { static_cast<int32_t>(offsetof(CEntityIdentity, m_EHandle)), false };
                return true;
            case kField_m_nameStringableIndex:
                key = { static_cast<int32_t>(offsetof(CEntityIdentity, m_nameStringableIndex)), true };
                return true;
            case kField_m_name:
                key = { static_cast<int32_t>(offsetof(CEntityIdentity, m_name)), false };
                return true;
            case kField_m_designerName:
                key = { static_cast<int32_t>(offsetof(CEntityIdentity, m_designerName)), false };
                return true;
            case kField_m_flags:
                key = { static_cast<int32_t>(offsetof(CEntityIdentity, m_flags)), false };
                return true;
            case kField_m_worldGroupId:
                key = { static_cast<int32_t>(offsetof(CEntityIdentity, m_worldGroupId)), false };
                return true;
            case kField_m_fDataObjectTypes:
                key = { static_cast<int32_t>(offsetof(CEntityIdentity, m_fDataObjectTypes)), false };
                return true;
            case kField_m_PathIndex:
                key = { static_cast<int32_t>(offsetof(CEntityIdentity, m_PathIndex)), false };
                return true;
            case kField_m_pAttributes:
                // m_pAttributes is private in the SDK header, so we bind the
                // confirmed server schema offset directly here.
                key = { 0x48, false };
                return true;
            case kField_m_pPrev:
                key = { static_cast<int32_t>(offsetof(CEntityIdentity, m_pPrev)), false };
                return true;
            case kField_m_pNext:
                key = { static_cast<int32_t>(offsetof(CEntityIdentity, m_pNext)), false };
                return true;
            case kField_m_pPrevByClass:
                key = { static_cast<int32_t>(offsetof(CEntityIdentity, m_pPrevByClass)), false };
                return true;
            case kField_m_pNextByClass:
                key = { static_cast<int32_t>(offsetof(CEntityIdentity, m_pNextByClass)), false };
                return true;
            default:
                return false;
        }
    }

    if (classKey == kCEntityInstance)
    {
        switch (memberKey)
        {
            case kField_m_iszPrivateVScripts:
                key = { static_cast<int32_t>(offsetof(CEntityInstance, m_iszPrivateVScripts)), false };
                return true;
            case kField_m_pEntity:
                key = { static_cast<int32_t>(offsetof(CEntityInstance, m_pEntity)), false };
                return true;
            case kField_m_pKeyValues:
                key = { static_cast<int32_t>(offsetof(CEntityInstance, m_pKeyValues)), false };
                return true;
            case kField_m_CScriptComponent:
                key = { static_cast<int32_t>(offsetof(CEntityInstance, m_CScriptComponent)), false };
                return true;
            default:
                return false;
        }
    }

    return false;
}
} // namespace

template <typename TVisitor>
static bool VisitSchemaFields(SchemaClassFieldBlockData_t* pFieldBlock, TVisitor&& visitor)
{
    for (auto* pBlock = pFieldBlock; pBlock != nullptr; pBlock = pBlock->m_pNext)
    {
        for (uint64 i = 0; i < pBlock->m_nFieldCount; ++i)
        {
            if (visitor(pBlock->m_pFields[i], pBlock)) return true;
        }
    }

    return false;
}

bool IsFieldNetworked(const SchemaClassFieldData_t& field)
{
    // Valve changed field metadata layout in the April 2026 update.
    // Offsets are now encoded directly in SchemaClassFieldData_t, but the
    // network metadata payload behind m_pszMetadataBlob still needs a separate decode path.
    (void)field;
    return false;
}

static bool InitSchemaFieldsForClass(SchemaTableMap_t* tableMap, const char* className, uint32_t classKey)
{
    CSchemaSystemTypeScope* pType = counterstrikesharp::globals::schemaSystem->FindTypeScopeForModule(MODULE_PREFIX "server" MODULE_EXT);

    if (!pType) return false;

    SchemaClassInfoData_t* pClassInfo = pType->FindDeclaredClass(className).Get();

    if (!pClassInfo)
    {
        SchemaKeyValueMap_t* map = new SchemaKeyValueMap_t(0, 0, DefLessFunc(uint32_t));
        tableMap->Insert(classKey, map);

        Warning("InitSchemaFieldsForClass(): '%s' was not found!\n", className);
        return false;
    }

    SchemaKeyValueMap_t* keyValueMap = new SchemaKeyValueMap_t(0, 0, DefLessFunc(uint32_t));
    tableMap->Insert(classKey, keyValueMap);

    VisitSchemaFields(pClassInfo->m_pFields, [&](SchemaClassFieldData_t& field, SchemaClassFieldBlockData_t*) {
        if (!field.m_pszName || field.m_pszName[0] == '\0')
        {
            return false;
        }

        if (!field.HasDirectOffset())
        {
            return false;
        }

        keyValueMap->Insert(hash_32_fnv1a_const(field.m_pszName), { field.m_nSingleInheritanceOffset, IsFieldNetworked(field) });
        return false;
    });

    return true;
}

int16_t schema::FindChainOffset(const char* className)
{
    CSchemaSystemTypeScope* pType = counterstrikesharp::globals::schemaSystem->FindTypeScopeForModule(MODULE_PREFIX "server" MODULE_EXT);

    if (!pType) return false;

    auto* pClassInfo = pType->FindDeclaredClass(className).Get();

    int16_t chainOffset = 0;

    VisitSchemaFields(pClassInfo->m_pFields, [&](SchemaClassFieldData_t& field, SchemaClassFieldBlockData_t*) {
        if (!field.m_pszName || !field.HasDirectOffset()) return false;

        if (V_strcmp(field.m_pszName, "__m_pChainEntity") == 0)
        {
            chainOffset = static_cast<int16_t>(field.m_nSingleInheritanceOffset);
            return true;
        }
        return false;
    });

    return chainOffset;
}

SchemaKey schema::GetOffset(const char* className, uint32_t classKey, const char* memberName, uint32_t memberKey)
{
    SchemaKey nativeKey{};
    if (TryGetNativeEntitySchemaOffset(classKey, memberKey, nativeKey))
    {
        return nativeKey;
    }

    static SchemaTableMap_t schemaTableMap(0, 0, DefLessFunc(uint32_t));
    int16_t tableMapIndex = schemaTableMap.Find(classKey);
    if (!schemaTableMap.IsValidIndex(tableMapIndex))
    {
        if (InitSchemaFieldsForClass(&schemaTableMap, className, classKey)) return GetOffset(className, classKey, memberName, memberKey);

        return { 0, 0 };
    }

    SchemaKeyValueMap_t* tableMap = schemaTableMap[tableMapIndex];
    int16_t memberIndex = tableMap->Find(memberKey);
    if (!tableMap->IsValidIndex(memberIndex))
    {
        return { 0, 0 };
    }

    return tableMap->Element(memberIndex);
}

void NetworkStateChanged(uintptr_t chainEntity, uint32_t offset, uint32_t nArrayIndex, uint32_t nPathIndex)
{
    CNetworkStateChangedInfo info(offset, nArrayIndex, nPathIndex);

    if (counterstrikesharp::globals::NetworkStateChanged)
        counterstrikesharp::globals::NetworkStateChanged(reinterpret_cast<void*>(chainEntity), info);
}

void SetStateChanged(uintptr_t pEntity, uint32_t offset, uint32_t nArrayIndex, uint32_t nPathIndex)
{
    CNetworkStateChangedInfo info(offset, nArrayIndex, nPathIndex);

    static auto fnOffset = counterstrikesharp::globals::gameConfig->GetOffset("SetStateChanged");
    CALL_VIRTUAL(void, fnOffset, (void*)pEntity, &info);
}
