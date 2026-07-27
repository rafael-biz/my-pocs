let count = 0;

function App() {
  function handleClick() {
    count = count + 1;
    console.log(count);
  }

  return (
    <div>
      <h1>How Not to Update the UI</h1>
      <p>Count: {count}</p>
      <button onClick={handleClick}>Increment</button>
      <p>Open the console: the variable changes, but the UI doesn't.</p>
    </div>
  );
}

export default App;
