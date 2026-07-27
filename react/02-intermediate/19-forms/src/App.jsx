import { useState } from "react";

function App() {
  const [name, setName] = useState("");

  function handleChange(event) {
    setName(event.target.value);
  }

  function handleSubmit(event) {
    event.preventDefault();
    console.log(`Submitted: ${name}`);
  }

  return (
    <div>
      <h1>Forms</h1>
      <form onSubmit={handleSubmit}>
        <label htmlFor="name">Name</label>
        <input id="name" type="text" value={name} onChange={handleChange} />
        <button type="submit">Submit</button>
      </form>
      <p>You typed: {name}</p>
    </div>
  );
}

export default App;
