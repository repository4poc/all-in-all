'as client';

import { fetchSingleProduct } from '@/utils/actions';
import Image from 'next/image';
import { formatCurrency } from '@/utils/formats';

import { auth } from '@clerk/nextjs/server';

async function SingleProductPage({
  params,
}: {
  params: Promise<{ id: string }>;
}) {
  const { id } = await params;
  const product = await fetchSingleProduct(id);
  const { name, image, company, description, price } = product;
  const dollarsAmount = formatCurrency(price);
  return (
    <section>
      <div className='mt-6 grid gap-y-8 lg:grid-cols-2 lg:gap-x-16'>
        <div className='relative aspect-square w-full'>
          <Image
            src={image}
            alt={name}
            fill
            className='rounded object-cover'
            sizes='(max-width:508px) 100vw,(max-width:700px) 25vw,25vw'
            priority
          />
        </div>
        <div>
          <div className='flex gap-x-8 items-center'>
            <h1 className='capitalize text-3xl font-bold'>{name} </h1>
          </div>
          <h4 className='text-xl mt-2'>{company}</h4>
          <p className='mt-3 text-md bg-muted inline-block p-2 rounded'>
            {dollarsAmount}
          </p>
          <p className='mt-6 leading-8 text-muted-foreground'>{description}</p>
        </div>
      </div>
    </section>
  );
}
export default SingleProductPage;
