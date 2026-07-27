# First Component

Builds on [01-hello-world](../01-hello-world). This app explores creating and
using a custom React component.

## Idea

- A component is a JavaScript function whose name is capitalized (`Greeting`,
  not `greeting`) and which returns JSX.
- Once defined, a component is used like any other JSX tag: `<Greeting />`.
- Because it's just a function, it can be reused as many times as needed —
  see `App.jsx`, where `<Greeting />` is rendered twice.
- A component's JSX must return a single root element (here, the outer
  `<div>` wraps the `<h2>` and `<p>`).

See [src/App.jsx](src/App.jsx).

## Getting started

```
bun install
bun run dev
```

Then open http://localhost:5173 or Start Debugging with VSCode.
