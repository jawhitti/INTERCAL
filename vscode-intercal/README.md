# INTERCAL for Visual Studio Code

Syntax highlighting and source-level debugging for INTERCAL, the Computer Language with No Pronounceable Acronym.

## Installation

### Syntax Highlighting

Copy the `vscode-intercal` folder to your VS Code extensions directory:

```
cp -r vscode-intercal ~/.vscode/extensions/intercal
```

Reload VS Code. Files with the `.i` extension will get INTERCAL syntax highlighting.

### Debugging

1. Install the **C# Dev Kit** extension from the VS Code marketplace.
2. The `.vscode/` folder in the project root contains `launch.json`, `tasks.json`, and `settings.json` preconfigured for INTERCAL debugging.

## Syntax Highlighting

Open any `.i` file. The extension highlights:

- **Keywords**: `DO`, `PLEASE`, `NOT`, `GIVE UP`, `READ OUT`, `WRITE IN`, `COME FROM`, etc.
- **Operators**: `$` (mingle), `~` (select), `&` (AND), `?` (XOR), `V`/`v` (OR), `|` (rotate), `-` (flip)
- **Variables**: `.1` (spot), `:1` (two-spot), `::1` (four-spot), `,1` (tail), `;1` (hybrid), `;;1` (double-hybrid), `[]1` (box)
- **Constants**: `#1` (16-bit), `##1` (32-bit), `####1` (64-bit)
- **Labels**: `(100)`, `(4920558940556964150)`, etc.
- **Gerunds**: NEXTING, CALCULATING, BOXING, FEEDING, PETTING, etc.
- **Splatted statements** highlighted as errors

## Debugging

### Getting Started

1. Open an `.i` file (e.g., `samples/collatz.i`).
2. Set breakpoints by clicking the gutter.
3. Press **F5** to compile, build, and launch under the debugger.
4. The program runs in the **Terminal** tab — type input there when prompted by `WRITE IN`.

### Stepping

- **Step Over** (F10) advances one INTERCAL statement at a time.
- **Step Into** (F11) on a `DO (label) NEXT` behaves like Step Over due to the threading model — it executes the NEXT and stops at the next visible statement. Set a breakpoint at the target label as an alternative.
- **Continue** (F5) runs to the next breakpoint.

### Inspecting Variables

INTERCAL variables appear as first-class locals in the **Variables** panel:

| INTERCAL | Debugger Local | Width |
|----------|---------------|-------|
| `.1`     | `dot_1`       | 16-bit |
| `:1`     | `colon_1`     | 32-bit |
| `::1`    | `dcolon_1`    | 64-bit |

Variables are updated after each statement executes. Only variables referenced in the program are shown.

To inspect variables not in the Locals panel, use the **Debug Console**:

```
frame.ExecutionContext.GetVarValue(".1")
```

### Known Limitations

- **Step Into on NEXT**: The runtime uses a thread-per-NEXT model, so the debugger cannot follow execution into a `DO (label) NEXT` call. Setting a breakpoint at the target label works as an alternative.
- **INTERCAL expressions**: The Debug Console evaluates C# expressions, not INTERCAL. You cannot evaluate `:1 ~ #65535` directly.
- **Arrays**: Array variables (`,1`, `;1`) are not shown in the Locals panel. Use the Debug Console to inspect them.
- **E774**: The build task compiles with `-b` to suppress random compiler bugs during debugging.

## Compiling Without the Debugger

```
dotnet run --project cringe -- -b samples/collatz.i
dotnet build ~tmp.csproj
dotnet run --project ~tmp.csproj
```

The `-b` flag disables E774 (random compiler bugs).

## i# System Library

Programs can call the i# syslib routines using ASCII-encoded 64-bit labels:

| Operation | 16-bit | 32-bit | 64-bit |
|-----------|--------|--------|--------|
| Add       | `(4702958889031696384)` | `(4702958897554522112)` | `(4702958910472978432)` |
| Subtract  | `(5569068542595249664)` | `(5569068542595379712)` | `(5569068542595576832)` |
| Multiply  | `(6073470532629640704)` | `(6073470532629770752)` | `(6073470532629967872)` |
| Divide    | `(4920558940556964150)` | `(4920558940556964658)` | `(4920558940556965428)` |
| Modulo    | `(5570746397223760182)` | `(5570746397223760690)` | `(5570746397223761460)` |
| Random    | `(5927104639891484982)` | `(5927104639891485490)` | `(5927104639891486260)` |

These labels are the ASCII encoding of `ADD16`, `MINUS32`, `DIVIDE64`, etc. packed into 8 bytes.

## INTERCAL Character Names

| Character | Name |
|-----------|------|
| `.` | spot |
| `:` | two-spot |
| `::` | four-spot |
| `,` | tail |
| `;` | hybrid |
| `;;` | double-hybrid |
| `#` | mesh (= `\|`-`\|`- = identity) |
| `=` | uneven bars |
| `$` | big money (mingle) |
| `~` | squid (select) |
| `&` | ampersand / bookworm |
| `V` | hybrid (or) |
| `?` | what (xor) |
| `\|` | stripper pole (rotate) |
| `-` | monkey bar (flip) |
| `'` | spark |
| `"` | rabbit ears |
| `(` | wax |
| `)` | wane |
| `<-` | angle-worm |
| `[]` | cat box |
