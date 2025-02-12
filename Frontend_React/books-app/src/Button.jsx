export default function Button({ button, onButtonClicked }) {
  const buttonClasses = {
    Add: "btn add",
    delete: "btn delete",
    update: "btn update",
  };

  return (
    <button className={buttonClasses[button]} onClick={onButtonClicked}>
      {button}
    </button>
  );
}
