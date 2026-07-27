import "./Greeting.css";

function Greeting({ name }) {
  return (
    <div className="greeting">
      <h2>Hi, {name}!</h2>
    </div>
  );
}

export default Greeting;
