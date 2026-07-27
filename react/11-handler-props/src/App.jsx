function Button({ onClick, label }) {
  return <button onClick={onClick}>{label}</button>;
}

function App() {
  function handleSave() {
    console.log("Saved!");
  }

  function handleDelete() {
    console.log("Deleted!");
  }

  return (
    <div>
      <h1>Handler Props</h1>
      <Button onClick={handleSave} label="Save" />
      <Button onClick={handleDelete} label="Delete" />
    </div>
  );
}

export default App;
