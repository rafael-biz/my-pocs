import { useEffect, useState } from "react";

function App() {
  const [count, setCount] = useState(0);

  useEffect(() => {
    document.title = `Count: ${count}`;
  }, [count]);

  useEffect(() => {
    const id = setInterval(() => {
      console.log("Still here...");
    }, 1000);

    return () => clearInterval(id);
  }, []);

  function handleClick() {
    setCount(count + 1);
  }

  return (
    <div>
      <h1>useEffect</h1>
      <p>Count: {count}</p>
      <button onClick={handleClick}>Increment</button>
      <p>Check the tab title, and the console for the interval log.</p>
    </div>
  );
}

export default App;
