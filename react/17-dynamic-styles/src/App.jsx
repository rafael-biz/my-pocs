import { useState } from "react";
import "./App.css";

function App() {
  const [isActive, setIsActive] = useState(false);

  function handleClick() {
    setIsActive(!isActive);
  }

  return (
    <div>
      <h1>Dynamic Styles</h1>
      <p className={isActive ? "active" : ""}>Styled with a dynamic className</p>
      <p style={{ color: isActive ? "green" : "gray" }}>Styled with an inline style</p>
      <button onClick={handleClick}>Toggle</button>
    </div>
  );
}

export default App;
