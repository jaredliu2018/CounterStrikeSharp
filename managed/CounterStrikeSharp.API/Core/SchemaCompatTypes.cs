/*
 *  This file is part of CounterStrikeSharp.
 *  CounterStrikeSharp is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  CounterStrikeSharp is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with CounterStrikeSharp.  If not, see <https://www.gnu.org/licenses/>. *
 */

using System;
using System.Runtime.CompilerServices;
using CounterStrikeSharp.API.Modules.Memory;
using CounterStrikeSharp.API.Modules.Utils;

namespace CounterStrikeSharp.API.Core;

public class CUtlLeanVector<T> : NativeObject
{
    public CUtlLeanVector(IntPtr pointer) : base(pointer)
    {
    }

    public unsafe ref int Count => ref Unsafe.AsRef<int>((void*)Handle);

    public unsafe ref int Allocated => ref Unsafe.AsRef<int>((void*)(Handle + sizeof(int)));

    public unsafe IntPtr Elements => Unsafe.Read<IntPtr>((void*)(Handle + (sizeof(int) * 2)));

    public int NumAllocated => Allocated & int.MaxValue;

    public bool IsExternallyAllocated => (Allocated & int.MinValue) != 0;
}

public class CEntityNameString : NativeObject
{
    public CEntityNameString(IntPtr pointer) : base(pointer)
    {
    }

    public unsafe ref ulong Value => ref Unsafe.AsRef<ulong>((void*)Handle);
}

public partial class PrecipitationFilter_t : NativeObject
{
    public PrecipitationFilter_t(IntPtr pointer) : base(pointer)
    {
    }

    public ref float MaxRadius => ref Schema.GetRef<float>(this.Handle, "PrecipitationFilter_t", "m_flMaxRadius");
}

public partial class DestructiblePartDamageRequest_t : NativeObject
{
    public DestructiblePartDamageRequest_t(IntPtr pointer) : base(pointer)
    {
    }

    public ref HitGroup_t HitGroup => ref Schema.GetRef<HitGroup_t>(this.Handle, "DestructiblePartDamageRequest_t", "m_nHitGroup");

    public ref int DamageLevel => ref Schema.GetRef<int>(this.Handle, "DestructiblePartDamageRequest_t", "m_nDamageLevel");

    public ref ushort DesiredHealth => ref Schema.GetRef<ushort>(this.Handle, "DestructiblePartDamageRequest_t", "m_nDesiredHealth");

    public ref EDestructibleParts_DestroyParameterFlags DestroyFlags => ref Schema.GetRef<EDestructibleParts_DestroyParameterFlags>(this.Handle, "DestructiblePartDamageRequest_t", "m_nDestroyFlags");

    public ref DamageTypes_t DamageType => ref Schema.GetRef<DamageTypes_t>(this.Handle, "DestructiblePartDamageRequest_t", "m_nDamageType");

    public ref float BreakDamage => ref Schema.GetRef<float>(this.Handle, "DestructiblePartDamageRequest_t", "m_flBreakDamage");

    public ref float BreakDamageRadius => ref Schema.GetRef<float>(this.Handle, "DestructiblePartDamageRequest_t", "m_flBreakDamageRadius");

    public Vector WsBreakDamageOrigin => Schema.GetDeclaredClass<Vector>(this.Handle, "DestructiblePartDamageRequest_t", "m_vWsBreakDamageOrigin");

    public Vector WsBreakDamageForce => Schema.GetDeclaredClass<Vector>(this.Handle, "DestructiblePartDamageRequest_t", "m_vWsBreakDamageForce");
}
