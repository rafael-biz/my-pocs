function Greeting(props) {
  return (
    <div>
      <h2>Hi, {props.name}!</h2>
      <p>You are {props.age} years old.</p>
    </div>
  );
}

function App() {
  return (
    <div>
      <h1>Component Props</h1>
      <Greeting name="Rafael" age={32} />
      <Greeting name="John" age={28} />
    </div>
  );
}

export default App;
