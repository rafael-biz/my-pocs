function Greeting() {
  return (
    <div>
      <h2>Hi there!</h2>
      <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
    </div>
  );
}

function App() {
  return (
    <div>
      <h1>Welcome!</h1>
      <Greeting />
      <Greeting />
    </div>
  );
}

export default App;
