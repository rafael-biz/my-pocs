function Greeting({ name, age }) {
  return (
    <div>
      <h2>Hi, {name}!</h2>
      <p>You are {age} years old.</p>
    </div>
  );
}

function App() {
  const person = { name: "Rafael", age: 32 };

  return (
    <div>
      <h1>Alternative Props Syntax</h1>
      <Greeting name="John" age={28} />
      <Greeting {...person} />
    </div>
  );
}

export default App;
