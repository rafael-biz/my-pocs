# Suspense POC

This proof-of-concept demonstrates the `templ`'s support for streaming and suspense-like rendering in Go web apps using:

- Deferred, asynchronous component rendering
- Named slots with fallback loading placeholders
- Streaming HTTP response delivery

The app renders a page immediately while three slot contents are generated concurrently in the background. Each slot shows a `Loading ...` placeholder until its async component is ready.

## What it does

1. Starts an HTTP server on `127.0.0.1:8080`.
2. When `/` is requested, it creates a channel for `SlotContents`.
3. It launches three goroutines simulating async work:
   - `A()` after 3 seconds
   - `B()` after 2 seconds
   - `C()` after 1 second
4. The template renders the page structure immediately and uses `@templ.Flush()` to flush partial output.
5. As each slot becomes available, the server streams its content into the matching slot.

## Generate

Regenerate the Go source from the template:

```powershell
templ generate
```

## Run

```powershell
go run .\main_templ.go
```

Then open `http://127.0.0.1:8080` in your browser.
