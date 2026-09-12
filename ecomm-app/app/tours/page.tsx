import axios from 'axios';
import Image from 'next/image';
import Link from 'next/link';

import mapsImg from '@/images/maps.jpg';
import UsersList from '@/components/UsersList';

type Tour = {
  id: string;
  name: string;
  info: string;
  image: string;
  price: string;
};

const url = 'https://www.course-api.com/react-tours-project';

const TourPage = async () => {
  const respose = await axios.get(url);
  const data: Tour[] = await respose.data;

  return (
    <section>
      <h1 className='text-blue-900 font-bold text-4xl'>Tour List</h1>
      <div className='grid md:grid-cols-3 gap-5 mt-2'>
        {data.map((tour) => {
          return (
            <div id={tour.id} key={tour.id}>
              <Link
                key={tour.id}
                href={`/tours/${tour.id}`}
                className='flex flex-col'
              >
                <div className='relative h-48 w-full'>
                  <Image
                    src={tour.image}
                    alt={tour.name}
                    fill
                    className='object-cover rounded'
                  />
                </div>

                <p className='mt-2 text-sm font-medium'>{tour.name}</p>
              </Link>
            </div>
          );
        })}
      </div>
      <UsersList />
    </section>
  );
};

export default TourPage;
