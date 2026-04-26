import { useState } from "react";
import QRCode from "qrcode";

function QRGenerator() {
  const [inputValue, setInputValue] = useState("");
  const [qrImage, setQrImage] = useState("");

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    const imageUrl = await QRCode.toDataURL(inputValue);
    setQrImage(imageUrl);
  };
  return (
    <>
      <div className="container qr-container">
        <h1>QR Generator</h1>
        <form onSubmit={handleSubmit}>
          <label className="form-label">Enter a URL</label>
          <div className="qrcode">
            <input
              type="text"
              placeholder="Type URL here..."
              value={inputValue}
              style={{ width: "300px" }}
              onChange={(e) => setInputValue(e.target.value)}
            />
          </div>

          <button type="submit" className="btn btn-primary">
            Generate QR
          </button>
        </form>
        <div className="qrcode mt-4">
          {qrImage && <img src={qrImage} alt="Generated QR Code" />}
        </div>
      </div>
    </>
  );
}

export default QRGenerator;
