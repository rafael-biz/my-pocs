import useCounter from "./hooks/useCounter.js";

function App() {
  const apples = useCounter(0);
  const oranges = useCounter(10);

  return (
    <div>
      <h1>Custom Hooks</h1>
      <p>Apples: {apples.count}</p>
      <button onClick={apples.increment}>+</button>
      <button onClick={apples.decrement}>-</button>
      <p>Oranges: {oranges.count}</p>
      <button onClick={oranges.increment}>+</button>
      <button onClick={oranges.decrement}>-</button>
    </div>
  );
}

export default App;
