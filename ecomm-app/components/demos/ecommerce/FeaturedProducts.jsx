import SectionTitle from '@/components/global/SectionTitle';
import ProductsGrid from '@/components/demos/ecommerce/ProductsGrid';
import productsArray from '@/utils/products';

async function FeaturedProducts() {
  //const products = await fetchFeaturedProducts();

  const products = productsArray;

  if (products.length === 0) return <EmptyList />;

  return (
    <section className='flex flex-col items-center justify-center'>
      <SectionTitle text='featured products' />
      <ProductsGrid products={products} />
    </section>
  );
}
export default FeaturedProducts;
