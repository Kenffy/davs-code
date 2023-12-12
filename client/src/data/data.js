import { FaCloud } from "react-icons/fa";
import { FaDatabase } from "react-icons/fa6";
import { FaMobile } from "react-icons/fa";
import { MdViewQuilt } from "react-icons/md";

export const Services = [
  {
    id: 1,
    name: "REST APIs",
    list: ["ASP.NET Core", "Express JS", "Python Flask", "Spring Boot"],
    icon: <FaCloud className="service-icon" />,
  },
  {
    id: 2,
    name: "UI Design",
    list: ["HTML, CSS, JS", "React JS", "Angular", "Blazor"],
    icon: <MdViewQuilt className="service-icon" />,
  },
  {
    id: 3,
    name: "Databases",
    list: ["MySQL", "Postgresql", "MongoDB", "Firebase", "MS SQL Server"],
    icon: <FaDatabase className="service-icon" />,
  },
  {
    id: 4,
    name: "Mobile",
    list: ["React Native", ".NET MAUI", "Xamarin"],
    icon: <FaMobile className="service-icon" />,
  },
];
