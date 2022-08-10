# .Net console demo - Roslyn to compile code with custom Analyzers

It compile the program text into a DLL using Roslyn compilers. It consider the custom Roslyn analyzers too as part of compilation.

# Specifications

- .Net version - .Net 6
- Nugets referenced
	- DotNet.Helpers
	- easyconsolestd
	- Microsoft.Extensions.Hosting

# Dependency injection

- Supported. Refer the [Program.cs](/src/Program.cs) file for more details
- The options are injected as dependency to the [MenuService](/src/MenuService.cs then those are invoked based on selection. 
