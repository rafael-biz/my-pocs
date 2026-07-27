import { useState } from "react";

function App() {
  const [count, setCount] = useState(0);
  const isEven = count % 2 === 0;

  function handleClick() {
    setCount(count + 1);
  }

  return (
    <div>
      <h1>Deriving State</h1>
      <p>Count: {count}</p>
      <p>The count is {isEven ? "even" : "odd"}.</p>
      <button onClick={handleClick}>Increment</button>
    </div>
  );
}

export default App;
