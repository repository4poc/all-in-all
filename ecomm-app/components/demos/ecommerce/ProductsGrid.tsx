import Link from 'next/link';
import Image from 'next/image';
import { Card, CardContent } from '@/components/ui/card';
import { ProductType } from '@/utils/products';
import { CardImage } from './CartItem';

function ProductsGrid({ products }: { products: ProductType[] }) {
  return (
    <div className='pt-12 grid gap-4 sm:grid-cols-1 lg:grid-cols-3'>
      {products.map((product) => {
        const { id, name, price, url } = product;
        const productId = product.id;
        const dollarsAmount = price;

        return (
          <article key={productId} className='group relative'>
            <Link href={`./ecommerce/products/${productId}`}>
              <CardImage product={product} />
            </Link>
          </article>
        );
      })}
    </div>
  );
}
export default ProductsGrid;
