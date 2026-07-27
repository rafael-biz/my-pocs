import { useState } from "react";

import Modal from "./components/Modal.jsx";

function App() {
  const [isOpen, setIsOpen] = useState(false);

  return (
    <div>
      <h1>Portals</h1>
      <button onClick={() => setIsOpen(true)}>Open modal</button>
      {isOpen && (
        <Modal>
          <p>I'm rendered into #modal-root, outside the #root DOM subtree.</p>
          <button onClick={() => setIsOpen(false)}>Close</button>
        </Modal>
      )}
    </div>
  );
}

export default App;
