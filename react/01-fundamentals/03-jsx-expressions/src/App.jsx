function Greeting() {
  const userName = "John";

  function getGreeting() {
    return "Good to see you";
  }

  return (
    <div>
      <h2>Hi there!</h2>
      <p>
        {getGreeting()}, {userName}!
      </p>
    </div>
  );
}

function App() {
  const pageTitle = "Welcome!";

  return (
    <div>
      <h1>{pageTitle}</h1>
      <Greeting />
    </div>
  );
}

export default App;
