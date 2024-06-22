import Accounts from "../accounts/Accounts";
import Button from "../button/Button";
import styles from "./AccountManagement.module.css";
import ManagementHeader from "./managementHeader/ManagementHeader";

function AccountManagement() {
  return (
    <>
      <ManagementHeader />
      <Accounts />
    </>
  );
}

export default AccountManagement;
