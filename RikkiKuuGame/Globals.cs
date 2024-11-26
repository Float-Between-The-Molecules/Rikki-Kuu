/*
Project-wide global using directives.

please put things here that are needed in the general case only, one-off things should go in the source file that requires it.

as a rule of thumb, godot types are at high risk of overheads due to cross-scripting translations and gdscript's VM.
it is highly preferable to use C# built-in collection types, threading and so on
*/


// godot engine
global using Godot;

// standard system namespaces etc
global using System;
global using System.Runtime.InteropServices;
global using System.Runtime.CompilerServices;

/*
frequently used compilation attributes

[MethodImpl(MethodImplOptions.AggressiveInlining)]
this is roughly the same usage / intention as `inline` keyword in C++

*/

// resolve ambiguity with System.Environment, which is less frequently intended
global using Environment = Godot.Environment;

// debug
global using System.Diagnostics;
global using System.Diagnostics.CodeAnalysis;

// concurrency
global using System.Threading;
global using System.Threading.Tasks;

// resolve ambiguity with Godot.Thread, which is seldom intended
global using Thread = System.Threading.Thread;

// generic programming & collections
global using System.Collections.Generic;
global using System.Linq;
global using System.Linq.Expressions;

// godot generic types shorthand, in the spirit of Godot.GD namespace
global using GC = Godot.Collections;

