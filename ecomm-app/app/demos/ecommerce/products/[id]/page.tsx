'as client';
export default function page({ params }: { params: { id: string } }) {
  //const product = fetch(params.id);
  //const {name,image,company,description,price} = product;
  //const dollarAmount = formatCurrency(price)

  console.log(`id: ${params.id}`);

  return (
    <>
      Product Page
      <p>{params.id}</p>
    </>
  );
}
