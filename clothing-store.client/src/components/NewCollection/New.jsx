import "./new.css";
import React from "react";
import Marquee from "react-fast-marquee";

const New = () => {
  return (
    <>
      <Marquee className="marquee" autoFill>
        <span className="marquee-text">New brand / </span>
      </Marquee>
      <div className="new-container">
        <div className="new-main-image-container">
          <img className="new-main-image" src="images/main.png" alt="" />
          <div className="new-main-image-info">
            <img className="new-main-image-logo" src="images/logo.png" />
            <span className="new-main-image-text">Already in our store</span>
          </div>
        </div>
        {/* <div className="new-extra-images">
          <img className="new-extra-img" src="images/photo_1.png" alt="" />
          <img className="new-extra-img" src="images/photo_2.png" alt="" />
          <img className="new-extra-img" src="images/photo_3.png" alt="" />
        </div> */}
      </div>
    </>
  );
};

export default New;
