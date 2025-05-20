# Orbital Language

Orbital is a simple, C#-based interpreted language designed for learning and experimentation. It features variables, arithmetic, control flow (if/else, while), and output.

## Features

- Variables and assignment
- Arithmetic operations: `gain` (+), `drain` (-), `amplify` (\*), `disperse` (/)
- Comparison: `align` (==), `disrupt` (!=), `above` (>), `below` (<), `safe` (>=), `unsafe` (<=)
- Boolean logic: `stable` (&&), `path` (||), `negate` (!)
- Control flow: `probe` (if), `scan` (else), `orbit` (while)
- Output: `uplink` (print)

## Syntax Reference

| Orbital Keyword | Meaning           | Example                      |
|-----------------|------------------|------------------------------|
| `gain`          | Addition         | `x gain 1`                   |
| `drain`         | Subtraction      | `x drain 1`                  |
| `amplify`       | Multiplication   | `x amplify 2`                |
| `disperse`      | Division         | `x disperse 2`               |
| `align`         | Equal            | `x align 1`                  |
| `disrupt`       | Not equal        | `x disrupt 1`                |
| `above`         | Greater than     | `x above 1`                  |
| `below`         | Less than        | `x below 1`                  |
| `safe`          | Greater or equal | `x safe 1`                   |
| `unsafe`        | Less or equal    | `x unsafe 1`                 |
| `stable`        | Logical AND      | `x stable y`                 |
| `path`          | Logical OR       | `x path y`                   |
| `negate`        | Logical/Num NOT  | `negate x`                   |
| `probe`         | If               | `probe(x align 1) {}`        |
| `scan`          | Else             | `scan {}`                    |
| `orbit`         | While            | `orbit(x below 5) {}`        |
| `uplink`        | Print/output     | `uplink(x)`                  |
| `void`          | Boolean false    | `x = void`                   |
| `signal`        | Boolean true     | `y = signal`                 |

## Running Code

To start the Orbital interpreter in interactive mode, use:
`OrbitalCore.exe`

To run an Orbital file, use:
`OrbitalCore.exe -f <filepath>`

Replace `<filePath>` with the path to your `.orbital` file.

Example Files
See the `ExampleCode/` directory for more sample programs.