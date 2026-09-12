'use client';

const Error = ({ error }: { error: Error }) => {
  console.log(error);
  return <div>error</div>;
};

export default Error;
