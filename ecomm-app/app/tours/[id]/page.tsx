const Page = async ({ params }: { params: Promise<{ id: string }> }) => {
  const { id } = await params;

  return <div>ID {id}</div>;
};

export default Page;
