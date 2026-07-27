function App() {
  function handleClick() {
    console.log("Button clicked!");
  }

  return (
    <div>
      <h1>Reacting to Events</h1>
      <button onClick={handleClick}>Click me</button>
      <button onClick={() => console.log("Inline handler!")}>Click me too</button>
    </div>
  );
}

export default App;
