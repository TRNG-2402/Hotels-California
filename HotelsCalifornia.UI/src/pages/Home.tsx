export default function Home() {
  return (
    <div
      style={{
        minHeight: "100vh",
        backgroundImage:
          "url('https://img.magnific.com/premium-photo/luxury-hotel_1015255-133056.jpg')",
        backgroundSize: "cover",
        backgroundPosition: "center",
        display: "flex",
        alignItems: "center",
        justifyContent: "center",
        padding: "40px",
        fontFamily: "Inter, Arial, sans-serif",
      }}
    >
      <div
        style={{
          backgroundColor: "rgba(255, 255, 255, 0.85)",
          padding: "40px",
          borderRadius: "12px",
          maxWidth: "600px",
          textAlign: "center",
          boxShadow: "0 8px 20px rgba(0,0,0,0.15)",
        }}
      >
        <h1
          style={{
            fontSize: "36px",
            fontWeight: 700,
            marginBottom: "10px",
            color: "#1f2937",
          }}
        >
          Find a Home Away From Home
        </h1>

        <p
          style={{
            fontSize: "16px",
            color: "#4b5563",
            marginBottom: "24px",
            lineHeight: "1.6",
          }}
        >
          Welcome to Hotels California - your destination for comfort, style, and
          unforgettable stays. Whether you're planning a relaxing getaway, a
          business trip, or a family vacation, we have the perfect hotel for
          you.
        </p>

        <button
          style={{
            padding: "14px 28px",
            backgroundColor: "#4f46e5",
            color: "white",
            border: "none",
            borderRadius: "8px",
            fontSize: "16px",
            fontWeight: 600,
            cursor: "pointer",
            transition: "0.2s",
          }}
          onClick={() => {
            window.location.href = "/Hotels";
          }}
          onMouseEnter={(e) => {
            (e.target as HTMLButtonElement).style.backgroundColor = "#4338ca";
          }}
          onMouseLeave={(e) => {
            (e.target as HTMLButtonElement).style.backgroundColor = "#4f46e5";
          }}
        >
          Book Now
        </button>
      </div>
    </div>
  );
}
