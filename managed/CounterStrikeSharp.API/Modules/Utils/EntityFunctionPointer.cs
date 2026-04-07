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

namespace CounterStrikeSharp.API.Modules.Utils
{
    public abstract class EntityFunctionPointer : NativeObject
    {
        protected EntityFunctionPointer(IntPtr pointer) : base(pointer)
        {
        }

        public unsafe IntPtr Address => Unsafe.Read<IntPtr>((void*)Handle);

        public unsafe IntPtr ThisAdjustment => Unsafe.Read<IntPtr>((void*)(Handle + IntPtr.Size));
    }

    public sealed class BASEPTR : EntityFunctionPointer
    {
        public BASEPTR(IntPtr pointer) : base(pointer)
        {
        }
    }

    public sealed class ENTITYFUNCPTR : EntityFunctionPointer
    {
        public ENTITYFUNCPTR(IntPtr pointer) : base(pointer)
        {
        }
    }

    public sealed class USEPTR : EntityFunctionPointer
    {
        public USEPTR(IntPtr pointer) : base(pointer)
        {
        }
    }
}
