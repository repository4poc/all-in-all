import CheckboxInput from '@/components/global/form/CheckboxInput';
import FormContainer from '@/components/global/form/FormContainer';
import FormInput from '@/components/global/form/FormInput';
import ImageInput from '@/components/global/form/ImageInput';
import PriceInput from '@/components/global/form/PriceInput';
import { SubmitButton } from '@/components/global/form/SubmitButton';
import TextAreaInput from '@/components/global/form/TextAreaInput';
import { Button } from '@/components/ui/button';
import { Input } from '@/components/ui/input';
import { Label } from '@/components/ui/label';
import { createProductAction } from '@/utils/actions';
import { Separator } from '@base-ui/react/separator';
import { faker } from '@faker-js/faker';

function CreateProductPage() {
  const name = faker.commerce.productName();
  const company = faker.company.name();
  const description = faker.lorem.paragraph({ min: 10, max: 12 });
  return (
    <section>
      <Separator className='mt-2' />
      <h1 className='text-2xl font-semibold mb-8 capitalize text-center'>
        create product
      </h1>
      <div className='border p-8 rounded-md'>
        <FormContainer action={createProductAction}>
          <div className='grid gap-4 md:grid-cols-2 my-2'>
            <FormInput
              type='text'
              name='name'
              label='product name'
              defaultValue={name}
            />
            <FormInput
              type='text'
              name='company'
              label='company'
              defaultValue={company}
            />
            <PriceInput />
            <ImageInput />
          </div>
          <TextAreaInput
            name='description'
            labelText='product description'
            defaultValue={description}
          />
          <div className='mt-6'>
            <CheckboxInput name='featured' label='featured' />
          </div>
          <SubmitButton text='create product' className='mt-8' />
        </FormContainer>
      </div>
    </section>
  );
}
export default CreateProductPage;
