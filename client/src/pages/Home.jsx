import React, { useEffect, useState } from "react";
import axios from "axios";
import HeaderBg from "../assets/images/background.png";
import NoProfile from "../assets/images/default-profile.jpg";
import "../assets/styles/pages/home.css";
import { Link } from "react-router-dom";

const BaseUrl = "https://localhost:5004/api/products";

export default function Home() {
  // const [courses, setCoures] = useState([]);
  // useEffect(() => {
  //   const loadCoursesAsync = async () => {
  //     try {
  //       const res = await axios.get(BaseUrl);
  //       if (res) {
  //         setCoures(res.data.result);
  //       }
  //     } catch (error) {
  //       console.log(error);
  //     }
  //   };
  //   loadCoursesAsync();
  // }, []);

  // console.log(courses);
  return (
    <div className="home">
      <div className="container s-header">
        <div className="s-header-left">
          <h1>Hello Welcome!</h1>
          <h3>
            Do you want to learn all about <span>web development</span> ?
          </h3>
          <h2>Come an code with me.</h2>
          <button>
            <Link to="/courses">Our Courses</Link>
          </button>
        </div>
        <div className="s-header-right">
          <img src={HeaderBg} alt="header cover" />
        </div>
      </div>

      <section id="greeting">
        <div className="container s-greeting">
          <div className="s-greeting-left">
            <div className="greeting-bg"></div>
            <div className="greeting-cover">
              <img src={NoProfile} alt="" />
            </div>
          </div>
          <div className="s-greeting-right">
            <h2>Hello! I am Davy</h2>
            <p>
              Lorem ipsum dolor sit amet consectetur adipisicing elit. Totam
              temporibus vel perferendis et voluptate quidem nihil eveniet, odit
              atque porro labore modi. Debitis tenetur ratione accusamus
              consectetur recusandae suscipit voluptatibus.
            </p>
            <p>
              Lorem ipsum dolor sit amet consectetur adipisicing elit. Totam
              temporibus vel perferendis et voluptate quidem nihil eveniet, odit
              atque porro labore modi. Debitis tenetur ratione accusamus
              consectetur recusandae suscipit voluptatibus. amet consectetur
              adipisicing elit. Totam temporibus vel perferendis et voluptate
              quidem nihil eveniet, odit atque porro labore modi.
            </p>
          </div>
        </div>
      </section>

      <section id="topics">
        <div className="container s-topics">
          <h2>Our Topics</h2>
          <ul className="topics-grid">
            <li className="topic-item">
              <h3>REST APIs</h3>
              <ul>
                <li>ASP.NET Core</li>
                <li>Express JS</li>
                <li>Python Flask</li>
                <li>Spring Boot</li>
              </ul>
            </li>
            <li className="topic-item">
              <h3>UI Design</h3>
              <ul>
                <li>HTML, CSS, JS</li>
                <li>React JS</li>
                <li>Angular</li>
                <li>Blazor</li>
              </ul>
            </li>
            <li className="topic-item">
              <h3>Databases</h3>
              <ul>
                <li>MySQL</li>
                <li>Postgresql</li>
                <li>MongoDB</li>
                <li>Firebase</li>
                <li>MS SQL Server</li>
              </ul>
            </li>
            <li className="topic-item">
              <h3>Mobile</h3>
              <ul>
                <li>React Native</li>
                <li>.NET MAUI</li>
                <li>Xamarin</li>
              </ul>
            </li>
          </ul>
        </div>
      </section>

      <section id="coures">
        <div className="container top-courses">Coures</div>
      </section>

      <section id="testimonials">
        <div className="container s-feedbacks">Feedbacks</div>
      </section>

      <section id="Contact">
        <div className="container s-contact">Contact</div>
      </section>
    </div>
  );
}
