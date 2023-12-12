import "../assets/styles/components/serviceitem.css";

export default function ServiceItem({ service }) {
  return (
    <li className="service-item">
      <h3>{service?.name}</h3>
      <div className="service-details">
        <ul>
          {service?.list.map((item) => (
            <li key={item}>{item}</li>
          ))}
        </ul>
        <>{service?.icon}</>
      </div>
    </li>
  );
}
