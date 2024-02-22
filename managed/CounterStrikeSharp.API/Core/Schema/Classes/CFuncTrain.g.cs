// <auto-generated />
#nullable enable
#pragma warning disable CS1591

using System;
using System.Diagnostics;
using System.Drawing;
using CounterStrikeSharp;
using CounterStrikeSharp.API.Modules.Events;
using CounterStrikeSharp.API.Modules.Entities;
using CounterStrikeSharp.API.Modules.Memory;
using CounterStrikeSharp.API.Modules.Utils;
using CounterStrikeSharp.API.Core.Attributes;

namespace CounterStrikeSharp.API.Core;

public partial class CFuncTrain : CBasePlatTrain
{
    public CFuncTrain (IntPtr pointer) : base(pointer) {}

	// m_hCurrentTarget
	[SchemaMember("CFuncTrain", "m_hCurrentTarget")]
	public CHandle<CBaseEntity> CurrentTarget => Schema.GetDeclaredClass<CHandle<CBaseEntity>>(this.Handle, "CFuncTrain", "m_hCurrentTarget");

	// m_activated
	[SchemaMember("CFuncTrain", "m_activated")]
	public ref bool Activated => ref Schema.GetRef<bool>(this.Handle, "CFuncTrain", "m_activated");

	// m_hEnemy
	[SchemaMember("CFuncTrain", "m_hEnemy")]
	public CHandle<CBaseEntity> Enemy => Schema.GetDeclaredClass<CHandle<CBaseEntity>>(this.Handle, "CFuncTrain", "m_hEnemy");

	// m_flBlockDamage
	[SchemaMember("CFuncTrain", "m_flBlockDamage")]
	public ref float BlockDamage => ref Schema.GetRef<float>(this.Handle, "CFuncTrain", "m_flBlockDamage");

	// m_flNextBlockTime
	[SchemaMember("CFuncTrain", "m_flNextBlockTime")]
	public ref float NextBlockTime => ref Schema.GetRef<float>(this.Handle, "CFuncTrain", "m_flNextBlockTime");

	// m_iszLastTarget
	[SchemaMember("CFuncTrain", "m_iszLastTarget")]
	public string LastTarget
	{
		get { return Schema.GetUtf8String(this.Handle, "CFuncTrain", "m_iszLastTarget"); }
		set { Schema.SetString(this.Handle, "CFuncTrain", "m_iszLastTarget", value); }
	}

}