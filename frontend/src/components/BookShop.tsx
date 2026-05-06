import Book from "./BookShop/Book";
import "./BookShop.css";

function BookShop() {
  const books = [
    { author: "varinder", img: "/assets/logo.png", title: "Epic Shit" },
    { author: "K S Makhan", img: "/assets/img1.jpg", title: "Epic Shit" },
    { author: "Raj Kumar", img: "/assets/vg_logo.svg", title: "Epic Shit" },
    { author: "Baiant Singh", img: "/assets/logo.png", title: "Epic Shit" },
    { author: "Paash", img: "/assets/logo.png", title: "Epic Shit" },
  ];

  return (
    <div className="container py-4">
      <div className="row g-4">
        {books.map((_, index) => (
          <div className="col-12 col-sm-6 col-md-4 col-lg-3" key={index}>
            <Book
              author={books[index].author}
              title={books[index].title}
              img={books[index].img}
            />
          </div>
        ))}
      </div>
    </div>
  );
}

export default BookShop;
