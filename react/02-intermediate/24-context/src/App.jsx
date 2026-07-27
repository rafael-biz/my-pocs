import { useContext, useState } from "react";
import ThemeContext from "./ThemeContext.js";
import "./App.css";

function ThemedButton() {
  const theme = useContext(ThemeContext);

  return <button className={`themed-button ${theme}`}>Current theme: {theme}</button>;
}

function Toolbar() {
  return (
    <div>
      <ThemedButton />
    </div>
  );
}

function App() {
  const [theme, setTheme] = useState("light");

  function handleToggle() {
    setTheme(theme === "light" ? "dark" : "light");
  }

  return (
    <ThemeContext.Provider value={theme}>
      <h1>Context</h1>
      <Toolbar />
      <button onClick={handleToggle}>Toggle theme</button>
    </ThemeContext.Provider>
  );
}

export default App;
