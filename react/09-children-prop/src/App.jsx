import Card from "./components/Card.jsx";

function App() {
  return (
    <div>
      <h1>Children Prop</h1>
      <Card>
        <h2>Hi, Rafael!</h2>
        <p>This content is passed between Card's opening and closing tags.</p>
      </Card>
      <Card>
        <h2>Hi, John!</h2>
        <p>Card doesn't know what's inside it - it just renders `children`.</p>
      </Card>
    </div>
  );
}

export default App;
