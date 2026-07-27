import logo from "./assets/logo.svg";

function App() {
  const description = "The JSX logo";
  const isLarge = true;

  return (
    <div>
      <h1>Dynamic Attributes</h1>
      <img src={logo} alt={description} className={isLarge ? "large" : "small"} />
    </div>
  );
}

export default App;
