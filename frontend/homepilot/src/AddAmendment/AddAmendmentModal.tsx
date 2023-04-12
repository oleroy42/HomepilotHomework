import React from "react";
import AddAmendmentForm from "./AddAmendmentForm.tsx";
import { Amendment } from "../App";

const AddAmendmentModal = (
    {
        closeModal,
        setNewAmendment
    }: {
        closeModal: () => void;
        setNewAmendment: (amendment: Amendment) => void;
    }
) => {

    return (
        <div className='modal is-active'>
            <div className='modal-background' onClick={closeModal}></div>
            <button className='modal-close is-large' aria-label="close" onClick={closeModal}></button>            

            <div className='modal-card' style={{width: '750px'}}>
                <section id='modalBody' className='modal-card-body'>
                    <AddAmendmentForm closeModal={closeModal} setNewAmendment={setNewAmendment}/>
                </section>
            </div>
        </div>   

    );
}

export default AddAmendmentModal;