import { useEffect, useRef, useState } from "react";

function App() {
  const inputRef = useRef(null);
  const renderCount = useRef(0);
  const [text, setText] = useState("");

  useEffect(() => {
    renderCount.current = renderCount.current + 1;
  });

  function handleFocusClick() {
    inputRef.current.focus();
  }

  return (
    <div>
      <h1>Refs</h1>
      <input ref={inputRef} value={text} onChange={(event) => setText(event.target.value)} />
      <button onClick={handleFocusClick}>Focus input</button>
      <p>This component has rendered {renderCount.current} times.</p>
    </div>
  );
}

export default App;
