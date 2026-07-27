# Custom Event Arguments

This app explores passing custom arguments to an event handler. Writing
`onClick={handleGreet("Rafael")}` would call the function immediately during
render, so it's wrapped in an inline arrow function instead, which defers
the call until the click actually happens.

## Getting started

```
bun install
bun run dev
```

Then open http://localhost:5173 or Start Debugging with VSCode.
