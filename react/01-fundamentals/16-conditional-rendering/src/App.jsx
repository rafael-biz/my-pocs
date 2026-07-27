import { useState } from "react";

function Alert({ message }) {
  if (!message) {
    return null;
  }

  return <p>{message}</p>;
}

function App() {
  const [isLoggedIn, setIsLoggedIn] = useState(false);

  function handleClick() {
    setIsLoggedIn(!isLoggedIn);
  }

  return (
    <div>
      <h1>Conditional Rendering</h1>
      {isLoggedIn ? <p>Welcome back!</p> : <p>Please log in.</p>}
      {isLoggedIn && <p>You have new messages.</p>}
      <Alert message={isLoggedIn ? "" : "You are not logged in."} />
      <button onClick={handleClick}>{isLoggedIn ? "Log out" : "Log in"}</button>
    </div>
  );
}

export default App;
