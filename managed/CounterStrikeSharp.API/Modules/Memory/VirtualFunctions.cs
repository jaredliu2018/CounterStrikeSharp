using System;
using System.Collections.Generic;
using System.Linq;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Entities.Constants;
using CounterStrikeSharp.API.Modules.Memory.DynamicFunctions;
using CounterStrikeSharp.API.Modules.Utils;

namespace CounterStrikeSharp.API.Modules.Memory;

public static class VirtualFunctions
{
    public static MemoryFunctionVoid<IntPtr, HudDestination, string, IntPtr, IntPtr, IntPtr, IntPtr> ClientPrintFunc =
        new(GameData.GetSignature("ClientPrint"));

    public static Action<IntPtr, HudDestination, string, IntPtr, IntPtr, IntPtr, IntPtr> ClientPrint =
        ClientPrintFunc.Invoke;

    public static MemoryFunctionVoid<HudDestination, string, IntPtr, IntPtr, IntPtr, IntPtr> ClientPrintAllFunc =
        new(GameData.GetSignature("UTIL_ClientPrintAll"));

    public static Action<HudDestination, string, IntPtr, IntPtr, IntPtr, IntPtr> ClientPrintAll =
        ClientPrintAllFunc.Invoke;

    // void (*FnGiveNamedItem)(void* itemService,const char* pchName, void* iSubType,void* pScriptItem, void* a5,void* a6) = nullptr;
    public static MemoryFunctionWithReturn<IntPtr, string, IntPtr, IntPtr, IntPtr, IntPtr, IntPtr> GiveNamedItemFunc =
        new(GameData.GetSignature("GiveNamedItem"));

    public static Func<IntPtr, string, IntPtr, IntPtr, IntPtr, IntPtr, IntPtr> GiveNamedItem = GiveNamedItemFunc.Invoke;

    public static MemoryFunctionVoid<IntPtr, byte> SwitchTeamFunc =
        new(GameData.GetSignature("CCSPlayerController_SwitchTeam"));

    public static Action<IntPtr, byte> SwitchTeam = SwitchTeamFunc.Invoke;

    // void(*UTIL_Remove)(CEntityInstance*);
    public static MemoryFunctionVoid<IntPtr> UTIL_RemoveFunc =
        new(GameData.GetSignature("UTIL_Remove"));

    public static Action<IntPtr> UTIL_Remove = UTIL_RemoveFunc.Invoke;

    // void(*CBaseModelEntity_SetModel)(CBaseModelEntity*, const char*);
    public static MemoryFunctionVoid<IntPtr, string> SetModelFunc =
        new(GameData.GetSignature("CBaseModelEntity_SetModel"));

    public static Action<IntPtr, string> SetModel = SetModelFunc.Invoke;

    public static MemoryFunctionVoid<nint, RoundEndReason, float, nint, byte> TerminateRoundFunc =
        new(GameData.GetSignature("CCSGameRules_TerminateRound"));

    public static Action<IntPtr, RoundEndReason, float, nint, byte> TerminateRound = TerminateRoundFunc.Invoke;

    public static MemoryFunctionWithReturn<string, int, IntPtr> UTIL_CreateEntityByNameFunc =
        new(GameData.GetSignature("UTIL_CreateEntityByName"));

    public static Func<string, int, IntPtr> UTIL_CreateEntityByName = UTIL_CreateEntityByNameFunc.Invoke;

    public static MemoryFunctionVoid<IntPtr, IntPtr> CBaseEntity_DispatchSpawnFunc =
        new(GameData.GetSignature("CBaseEntity_DispatchSpawn"));

    public static Action<IntPtr, IntPtr> CBaseEntity_DispatchSpawn = CBaseEntity_DispatchSpawnFunc.Invoke;

    public static MemoryFunctionVoid<CBasePlayerController, CBasePlayerPawn, bool, bool> CBasePlayerController_SetPawnFunc = new (GameData.GetSignature("CBasePlayerController_SetPawn"));

	public static MemoryFunctionVoid<CEntityInstance, CTakeDamageInfo> CBaseEntity_TakeDamageOldFunc = new (GameData.GetSignature("CBaseEntity_TakeDamageOld"));
    public static Action<CEntityInstance, CTakeDamageInfo> CBaseEntity_TakeDamageOld = CBaseEntity_TakeDamageOldFunc.Invoke;
    
    public static MemoryFunctionWithReturn<CCSPlayer_WeaponServices, CBasePlayerWeapon, bool> CCSPlayer_WeaponServices_CanUseFunc = new(GameData.GetSignature("CCSPlayer_WeaponServices_CanUse"));
    public static Func<CCSPlayer_WeaponServices, CBasePlayerWeapon, bool> CCSPlayer_WeaponServices_CanUse = CCSPlayer_WeaponServices_CanUseFunc.Invoke;

    public static MemoryFunctionWithReturn<int, string, CCSWeaponBaseVData> GetCSWeaponDataFromKeyFunc = new(GameData.GetSignature("GetCSWeaponDataFromKey"));
    public static Func<int, string, CCSWeaponBaseVData> GetCSWeaponDataFromKey = GetCSWeaponDataFromKeyFunc.Invoke;

    public static MemoryFunctionWithReturn<CCSPlayer_ItemServices, CEconItemView, AcquireMethod, IntPtr, AcquireResult> CCSPlayer_ItemServices_CanAcquireFunc = new(GameData.GetSignature("CCSPlayer_ItemServices_CanAcquire"));
    public static Func<CCSPlayer_ItemServices, CEconItemView, AcquireMethod, IntPtr, AcquireResult> CCSPlayer_ItemServices_CanAcquire = CCSPlayer_ItemServices_CanAcquireFunc.Invoke;

    public static MemoryFunctionVoid<CCSPlayerPawnBase> CCSPlayerPawnBase_PostThinkFunc = new (GameData.GetSignature("CCSPlayerPawnBase_PostThink"));
    public static Action<CCSPlayerPawnBase> CCSPlayerPawnBase_PostThink = CCSPlayerPawnBase_PostThinkFunc.Invoke;
    
    public static MemoryFunctionVoid<CBaseTrigger, CBaseEntity> CBaseTrigger_StartTouchFunc = new (GameData.GetSignature("CBaseTrigger_StartTouch"));
    public static Action<CBaseTrigger, CBaseEntity> CBaseTrigger_StartTouch = CBaseTrigger_StartTouchFunc.Invoke;
    
    public static MemoryFunctionVoid<CBaseTrigger, CBaseEntity> CBaseTrigger_EndTouchFunc = new (GameData.GetSignature("CBaseTrigger_EndTouch"));
    public static Action<CBaseTrigger, CBaseEntity> CBaseTrigger_EndTouch = CBaseTrigger_EndTouchFunc.Invoke;
    
    public static MemoryFunctionVoid<IntPtr, IntPtr>  RemovePlayerItemFunc =
        new(GameData.GetSignature("CBasePlayerPawn_RemovePlayerItem"));
    public static Action<IntPtr, IntPtr> RemovePlayerItemVirtual = RemovePlayerItemFunc.Invoke;

    public static MemoryFunctionVoid<IntPtr, IntPtr, int, short, int> StateChangedFunc =
        new(GameData.GetSignature("StateChanged"));
    public static Action<IntPtr, IntPtr, int, short, int> StateChanged = StateChangedFunc.Invoke;

    public static MemoryFunctionVoid<IntPtr, int, long> NetworkStateChangedFunc = new(GameData.GetSignature("NetworkStateChanged"));
    public static Action<IntPtr, int, long> NetworkStateChanged = NetworkStateChangedFunc.Invoke;

}
