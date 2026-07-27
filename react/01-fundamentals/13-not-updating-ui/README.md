# How Not to Update the UI

This app explores why mutating a plain variable doesn't update what's on
screen: React has no way of knowing the variable changed, so nothing tells
it to re-render. This sets up the need for state, covered next in
`14-managing-state`.

## Getting started

```
bun install
bun run dev
```

Then open http://localhost:5173 or Start Debugging with VSCode.
