function App() {
  function handleGreet(name) {
    console.log(`Hello, ${name}!`);
  }

  return (
    <div>
      <h1>Custom Event Arguments</h1>
      <button onClick={() => handleGreet("Rafael")}>Greet Rafael</button>
      <button onClick={() => handleGreet("John")}>Greet John</button>
    </div>
  );
}

export default App;
