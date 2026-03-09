import type { LoanDashboardState, LoanAction } from "../types/loanActions";

export function loanReducer(
    state: LoanDashboardState,
    action: LoanAction,
): LoanDashboardState {
    switch (action.type) {
        case "FETCH_START":
            return { ...state, loading: true, error: null };

        case "FETCH_SUCCESS":
            return { ...state, loading: false, loans: action.loans };

        case "FETCH_ERROR":
            return { ...state, loading: false, error: action.message };

        case "APPROVE_LOAN":
            return {
                ...state,
                loans: state.loans.map((loan) =>
                    loan.id === action.loanId ? { ...loan, status: "Approved" } : loan,
                ),
                notificationCount: state.notificationCount + 1,
            };

        case "DENY_LOAN":
            return {
                ...state,
                loans: state.loans.map((loan) =>
                    loan.id === action.loanId ? { ...loan, status: "Denied" } : loan,
                ),
                notificationCount: state.notificationCount + 1,
            };

        case "FLAG_LOAN":
            return {
                ...state,
                loans: state.loans.map((loan) =>
                    loan.id === action.loanId ? { ...loan, status: "Flagged" } : loan,
                ),
                notificationCount: state.notificationCount + 1,
            };

        case "SET_FILTER":
            return { ...state, filter: action.filter };

        case "CLEAR_NOTIFICATIONS":
            return { ...state, notificationCount: 0 };

        default:
            return state;
    }
}
