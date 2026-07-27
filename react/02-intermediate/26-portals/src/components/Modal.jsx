import { createPortal } from "react-dom";

import "./Modal.css";

function Modal({ children }) {
  const modalRoot = document.getElementById("modal-root");

  return createPortal(<div className="modal">{children}</div>, modalRoot);
}

export default Modal;
