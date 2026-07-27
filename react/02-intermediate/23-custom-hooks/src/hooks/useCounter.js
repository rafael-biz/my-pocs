import { useState } from "react";

function useCounter(initialValue = 0) {
  const [count, setCount] = useState(initialValue);

  function increment() {
    setCount((current) => current + 1);
  }

  function decrement() {
    setCount((current) => current - 1);
  }

  return { count, increment, decrement };
}

export default useCounter;
