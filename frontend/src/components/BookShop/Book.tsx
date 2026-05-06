import "./Book.css";

type BookProps = {
  img: string;
  title: string;
  author: string;
};

const Book = (props: BookProps) => {
  return (
    <div className="card book-card h-100">
      <img src={props.img} alt="Book cover" className="card-img-top book-img" />

      <div className="card-body text-center">
        <h2 className="card-title">{props.title}</h2>
        <p className="card-text">{props.author}</p>
      </div>
    </div>
  );
};

export default Book;
