'use client';
import Link from 'next/link';
import Image from 'next/image';
import { Card, CardContent } from '@/components/ui/card';
import { ProductType } from '@/utils/products';
import { CardImage } from './CartItem';
import { useSearchParams } from 'next/navigation';
import { useState } from 'react';

function ProductsGrid({ products }: { products: ProductType[] }) {
  const searchParams = useSearchParams();

  console.log(`product: ${searchParams.get('product') ?? ''}`);

  const query = searchParams.get('product') ?? '';

  const filteredProducts = products.filter((product) =>
    product.name.toLowerCase().includes(query.toLowerCase()),
  );

  return (
    <div className='pt-12 grid gap-4 sm:grid-cols-1 lg:grid-cols-3'>
      {filteredProducts.map((product) => {
        const { id, name, price, image } = product;
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
