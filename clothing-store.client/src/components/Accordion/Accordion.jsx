import React, { useState } from "react";
import "./accordion.css";

const Accordion = ({ title, content }) => {
  const [isOpen, setIsOpen] = useState(false);

  const toggleAccordion = () => {
    setIsOpen(!isOpen);
  };

  return (
    <div className="accordion" onClick={toggleAccordion}>
      <div className="accordion-header">
        <span className="accordion-title">{title}</span>
        <span className={`accordion-arrow ${isOpen ? "open" : ""}`}>+</span>
      </div>

      <div className={`accordion-content ${isOpen ? "open" : ""}`}>
        {content}
      </div>
    </div>
  );
};

export default Accordion;
