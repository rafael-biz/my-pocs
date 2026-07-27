# Context

This app explores avoiding prop drilling with context: `Toolbar` never
receives or passes down a `theme` prop, yet `ThemedButton` reads it directly
via `useContext`, because both sit inside `ThemeContext.Provider`.

The "Toggle theme" button updates `App`'s own `theme` state, which flows
into the provider's `value`. Because `ThemedButton` styles itself from that
same context value, clicking it visibly swaps the button's colors - without
`Toolbar` ever knowing a theme exists.

## Getting started

```
bun install
bun run dev
```

Then open http://localhost:5173 or Start Debugging with VSCode.
