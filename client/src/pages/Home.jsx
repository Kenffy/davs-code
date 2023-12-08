import React, { useEffect, useState } from "react";
import axios from "axios";

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
      <section id="header">
        <div className="s-header container">Header</div>
      </section>

      <section id="greeting">
        <div className="container s-greeting">
          <h2>Hello Welcome! I am Davy</h2>
          <p>
            Lorem ipsum dolor sit amet consectetur adipisicing elit. Totam
            temporibus vel perferendis et voluptate quidem nihil eveniet, odit
            atque porro labore modi. Debitis tenetur ratione accusamus
            consectetur recusandae suscipit voluptatibus.
          </p>
        </div>
      </section>

      <section id="topics">
        <div className="container s-topics">
          <h4>Topics</h4>
          <ul>
            <li>Basics</li>
            <li>Front-end</li>
            <li>Back-end</li>
            <li>Mobile Development</li>
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
